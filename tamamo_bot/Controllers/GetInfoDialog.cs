using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Discovery;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace tamamo_bot.Controllers
{
    [Serializable]
    public class GetInfoDialog : IDialog<object>
    {
        protected int count = 1;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(GreetingAsync);
        }

        public async Task GreetingAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {

            var message = "おはようございます，ご主人様！今日はどんな情報が欲しいんです？";
            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context,
            IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            if (message.Text == "reset")
            {
                PromptDialog.Confirm(
                    context,
                    AfterResetAsync,
                    "Are you sure you want to reset the count?",
                    "Didn't get that!",
                    promptStyle:PromptStyle.None
                    );
            }
            else
            {
                await context.PostAsync(string.Format("{0}:You said {1}", this.count++, message.Text));
                context.Wait(MessageReceivedAsync);
            }
        }

        public async Task AfterResetAsync(IDialogContext context,
            IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count");
            }
            else
            {
                await context.PostAsync("Did not reset count");
            }
            
            context.Wait(MessageReceivedAsync);
        }
    }
}