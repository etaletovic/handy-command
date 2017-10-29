using System;
using Xunit;
using HandyCommand;

namespace Test
{
    public class CommandTest
    {
        public HandyCommand.Command Command1 { get; set; }
        public string Message => "Command1 executed";

        [Fact]
        public void LambdaActionPassed_WithoutObjectParameter()
        {

            string x = string.Empty;
            Command1 = new Command(() =>
            {
                x = Message;
            });

            Command1.Execute(null);

            Assert.True(x == Message);
        }

        [Fact]
        public void LambdaActionPassed_WithObjectParameter()
        {

            string x = string.Empty;
            Command1 = new Command((o) =>
            {
                x = Message;
            });

            Command1.Execute(null);

            Assert.True(x == Message);
        }

        [Fact]
        public void ActionObjectPassed_WithoutObjectParameter()
        {

            string x = string.Empty;

            Action action = new Action(() => { x = Message; });
            Command1 = new Command(action);
            Command1.Execute(null);

            Assert.True(x == Message);
        }
        [Fact]
        public void ActionObjectPassed_WithObjectParameter()
        {

            string x = string.Empty;

            var action = new Action<object>((o) => { x = Message; });
            Command1 = new Command(action);
            Command1.Execute(null);

            Assert.True(x == Message);
        }

        [Fact]
        public void LambdaActionAndFunctionPassed_WithoutObjectParameter()
        {

            string x = string.Empty;
            Command1 = new Command(() => { x = Message; },
                                   () => { return true; });

            Command1.Execute(null);

            Assert.True(Command1.CanExecute(null));


            Assert.True(x == Message);
        }

        [Fact]
        public void ObjectActionAndFunctionPassed_WithoutObjectParameter()
        {

            string x = string.Empty;

            Action action = new Action(() => { x = Message; });
            Func<bool> function = new Func<bool>(() => { return true; });


            Command1 = new Command(action, function);

            Command1.Execute(null);

            Assert.True(x == Message);
            Assert.True(Command1.CanExecute(null));

        }

        [Fact]
        public void LambdaActionAndFunctionPassed_WithObjectParameter()
        {

            string x = string.Empty;
            Command1 = new Command((o) => { x = Message; },
                                   (o) => { return true; });

            Command1.Execute(null);

            Assert.True(x == Message);
            Assert.True(Command1.CanExecute(null));

        }

        [Fact]
        public void ObjectActionAndFunctionPassed_WithObjectParameter()
        {

            string x = string.Empty;

            Action<object> action = new Action<object>((o) => { x = Message; });
            Func<object,bool> function = new Func<object, bool>((o) => { return true; });


            Command1 = new Command(action, function);

            Command1.Execute(null);

            Assert.True(x == Message);
            Assert.True(Command1.CanExecute(null));
        }
     
      

    }
    public class CommandTTests
    {
        public Command<string> Command { get; set; }

        [Fact]
        public void LambdaActionAndFunctionPassed_WithStringParameter()
        {
            string newMessage = string.Empty;

            Command = new Command<string>((message) =>
            {
                newMessage = message;
            }, (text) => { return text.Length > 0; });


            Assert.True(Command.CanExecute("Awesome"));
            Assert.False(Command.CanExecute(""));
        }


        [Fact]
        public void LambdaActionPassed_WithStringParameter()
        {
            string newMessage = string.Empty;

            Command = new Command<string>((message) => 
            {
                newMessage = message;
            });

            Command.Execute("AwesomeMessage");

            Assert.True(newMessage == "AwesomeMessage");
        }

    }
}
