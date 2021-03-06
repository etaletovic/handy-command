﻿using System;
using System.Threading.Tasks;
using HandyCommand;
using Xunit;

namespace Test
{
    public class AsyncCommandTest
    {
        public int EventCounter { get; set; }
        [Fact]
        public async Task AsyncCommand_IsAwaitable()
        {
            var x = 0;
            var command = new AsyncCommand(async () =>
            {
                await Task.Delay(2000);
                x = 1;
            });
            Assert.True(x == 0);
            await command.ExecuteAsync(null);
            Assert.True(x == 1);
        }

        [Fact]
        public async Task AsyncCommand_CanExecuteWorks()
        {
            var x = 0;
            var command = new AsyncCommand(async () =>
            {
                await Task.Delay(2000);
                x = 1;
            }, () => { return false; });
            Assert.True(x == 0);
            if (command.CanExecute(null))
                await command.ExecuteAsync(null);
            Assert.True(x == 0);
        }
        [Fact]
        public async Task AsyncCommand_EventsWork()
        {
            EventCounter = 0;
            var x = 0;
            var command = new AsyncCommand(async () =>
            {
                await Task.Delay(2000);
                x = 1;
            });

            command.CanExecuteChanged += Command_CanExecuteChanged;
            Assert.True(x == 0);
            await command.ExecuteAsync(null);
            Assert.True(x == 1);

            Assert.True(EventCounter == 2);
        }

        void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            EventCounter++;

        }
        [Fact]
        public async void AsyncCommand_CommandIsNotTriggeredIfExecuting()
        {

            var command = new AsyncCommand(async () =>
            {
                await Task.Delay(5000);
            });
            Assert.True(command.CanExecute(null), "Before execution start");
            command.ExecuteAsync(null);
            Assert.False(command.CanExecute(null), "After execution start");
            await Task.Delay(6000);
            Assert.True(command.CanExecute(null), "Execution should finish here");


        }

    }

    public class AsyncCommandTTest
    {
        public int EventCounter { get; set; }

        [Fact]
        public async Task AsyncCommandT_IsAwaitable()
        {
            var x = "";
            var text = "test";
            var command = new AsyncCommand<string>(async (message) =>
            {
                await Task.Delay(2000);
                x = message;
            });
            Assert.True(x == "");
            await command.ExecuteAsync(text);
            Assert.True(x == text);
        }

        [Fact]
        public async Task AsyncCommandT_CanExecuteWorks()
        {
            var x = "";
            var text = "test";

            var command = new AsyncCommand<string>(async (message) =>
            {
                await Task.Delay(2000);
                x = message;
            }, (message) => { return message.Length == 0; });
            Assert.True(x == "");
            if (command.CanExecute(text))
                await command.ExecuteAsync(text);
            Assert.True(x == "");
        }

        [Fact]
        public async Task AsyncCommandT_EventsWork()
        {
            EventCounter = 0;
            var command = new AsyncCommand<int>(async (milisec) =>
            {
                await Task.Delay(milisec);
            });

            command.CanExecuteChanged += (sender, e) => {
                EventCounter++;

            };
            await command.ExecuteAsync(2000);

            Assert.True(EventCounter == 2);
        }

        [Fact]
        public void AsyncCommandT_ShouldExecuteEventWorks()
        {
            var command = new AsyncCommand<int>(async (milisec) =>
            {
                await Task.Delay(milisec);
            });

            var a = 0;
            command.ShouldExecute += (sender, e) => {
                a++;
            };

            command.RaiseShouldExecute();

            Assert.True(a == 1);
        }


       
       
    }
}
