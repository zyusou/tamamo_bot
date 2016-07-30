using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;


namespace tamamo_bot.Controllers
{
    public enum CommandOptions
    {
        niconiko,
        hatebu,
        pixiv,
        Amazon,
        train
    }

    [Serializable]
    public class WebAPIFormFlow
    {
        public CommandOptions? Sandwich;

        public static IForm<WebAPIFormFlow> BuildForm()
        {
            return new FormBuilder<WebAPIFormFlow>()
                    .Message("あら!ご主人様!何について調べますか？")
                    .Build();
        }
    };
}