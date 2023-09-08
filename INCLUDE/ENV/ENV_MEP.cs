using System.Runtime.CompilerServices;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.ENV
{
    public abstract class ENV_MEP
    {
        public abstract List<string> ALLOW_IP { get; }
    }


    public class ENV_DEV : ENV_MEP
    {
        public override List<string> ALLOW_IP { get => ENV_MODEL.DEV_LS_IP; }
    }

    public class ENV_PRODUCT : ENV_MEP
    {
        public override List<string> ALLOW_IP { get => ENV_MODEL.PRO_LS_IP; }
    }


    public static class ENV_VARIBLE
    {
        public static ENV_MEP GET_ENV_VARIBLE()
        {
            var currentENV = Environment.GetEnvironmentVariable(ENV_MODEL.CURRENT_ENV);

            if (currentENV != null && string.Compare(currentENV, ENV_MODEL.ENV_DEV) == 0)
            {
                return new ENV_DEV();
            }
            else
            {
                return new ENV_PRODUCT();
            }
        }
    }


    public static class ENV_THIS
    {
        public static void ENVIROMENT(WebApplication app)
        {

            if (!app.Environment.IsDevelopment())
            {

                Environment.SetEnvironmentVariable(ENV_MODEL.CURRENT_ENV, ENV_MODEL.ENV_PRO);

                app.UseExceptionHandler("/error/fix");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                Environment.SetEnvironmentVariable(ENV_MODEL.CURRENT_ENV, ENV_MODEL.ENV_DEV);

            }
        }

        public static bool IS_DEV()
        {
            if (string.Compare(Environment.GetEnvironmentVariable(ENV_MODEL.CURRENT_ENV), ENV_MODEL.ENV_DEV) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
