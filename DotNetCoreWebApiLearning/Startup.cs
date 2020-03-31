using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.OpenApi.Models;
using DotNetCoreWebApiLearning.Services;
using DotNetCoreWebApiLearning.Data;
using System.Reflection;
using System.IO;
using Polly;
using System.Net.Http;

namespace DotNetCoreWebApiLearning
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddDbContext<DataDbContext>(option =>
            {
                option.UseSqlite("Data Source=DotNetCoreWebApiLearning.db");
            });

            // AutoMapperӳ��
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "web api", Version = "v1" });

                // ʹ�÷����ȡxml�ļ�����������ļ���·��
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // ����xmlע��. �÷����ڶ����������ÿ�������ע�ͣ�Ĭ��Ϊfalse.
                c.IncludeXmlComments(xmlPath, true);
            });

            #region polly

            var fallbackResponse = new HttpResponseMessage();
            fallbackResponse.Content = new StringContent("fallback");
            fallbackResponse.StatusCode = System.Net.HttpStatusCode.TooManyRequests;

            //����
            services.AddHttpClient("github", c =>
            {
                //��ַ
                c.BaseAddress = new System.Uri("https://api.github.com/");
                // Github API versioning
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                // Github requires a user-agent
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
                //AddTransientHttpErrorPolicy��Ҫ�Ǵ���Http����Ĵ�����HTTP 5XX ��״̬�룬HTTP 408 ��״̬�� �Լ�System.Net.Http.HttpRequestException�쳣
            }).AddTransientHttpErrorPolicy(p =>
                //WaitAndRetryAsync��������˼�ǣ�ÿ������ʱ�ȴ���˯�߳���ʱ�䡣
                p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600))
            );
           
            services.AddHttpClient("test", c =>
            {
                //��ַ
                c.BaseAddress = new System.Uri("http://localhost:5000/");
                // Github API versioning
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                // Github requires a user-agent
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            })
            //����
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().FallbackAsync(fallbackResponse, async b =>
            {
                Console.WriteLine($"fallback here {b.Exception.Message}");
            }))
            //�۶�
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().CircuitBreakerAsync(2, TimeSpan.FromSeconds(4), (ex, ts) =>
            {
                Console.WriteLine($"break here {ts.TotalMilliseconds}");
            }, () =>
            {
                Console.WriteLine($"reset here ");
            }))
            //��ʱ
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "web api doc v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
