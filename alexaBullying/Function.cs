using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;
using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace alexaBullying
{
    public class Function
    {
        string phrase = "";
        bool practice1Started = false;
        bool practice2Started = false;
        bool practice3Started = false;

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            SkillResponse response = new SkillResponse();
            response.Response = new ResponseBody();
            response.Response.ShouldEndSession = false;
            IOutputSpeech innerResponse = null;
            var log = context.Logger;
            log.LogLine($"Skill Request Object:");
            log.LogLine(JsonConvert.SerializeObject(input));


            if (input.GetRequestType() == typeof(LaunchRequest))
            {
                log.LogLine($"Default LaunchRequest made: 'Alexa, Stand up to a bully");
                innerResponse = new PlainTextOutputSpeech();
                (innerResponse as PlainTextOutputSpeech).Text = "Welcome to Comforting Comebacks, " +
                    "the number one guide for standing up to bullies effectively. " +
                    "I will simulate a bully and help you practice the best ways to respond. " +
                    "For example, a bully says Hey, Dummy and your comeback would be, Talk to the hand. " +
                    "Say continue to begin learning.";

            }
            else if (input.GetRequestType() == typeof(IntentRequest))
            {
                var intentRequest = (IntentRequest)input.Request;
                if (practice1Started == false  && practice2Started == false && practice3Started == false)
                {
                    switch (intentRequest.Intent.Name)
                    {
                        case "AMAZON.CancelIntent":
                            log.LogLine($"AMAZON.CancelIntent: send StopMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Goodbye!";
                            response.Response.ShouldEndSession = true;
                            break;
                        case "StopIntent":
                            log.LogLine($"AMAZON.StopIntent: send StopMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Goodbye!";
                            response.Response.ShouldEndSession = true;
                            break;
                        case "AMAZON.HelpIntent":
                            log.LogLine($"AMAZON.HelpIntent: send HelpMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "add later";
                            break;
                        case "YesIntent":
                            log.LogLine($"Answer Yes");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Here we go. ,beep, ,beep, ,beep, Hey, dummy";
                            practice1Started = true;
                            break;
                        case "NoIntent":
                            log.LogLine($"Answer no");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "If you would like to hear the phrases again say repeat. If you are done" +
                                " standing up to bullying then say exit or stop";
                            practice1Started = false;
                            break;
                        case "ContinueIntent":
                            log.LogLine($"choose to continue");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "The following comebacks work best when you say" +
                                " them calmy with a hint of sarcasm, then walk away.  When I say hey, dummy, respond immediately " +
                                "by saying of the three following phrases.  Option 1, talk to the hand.  Option 2, Wow! Now I know why " +
                                "everyone says that stuff about you.  Option 3, Do you think you could star in the next Mean Girls movie?  " +
                                "Pick one phrase and say it back now.  If you need to hear them again, say repeat";
                            break;
                        case "RepeatIntent":
                            log.LogLine($"phrases repeated");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Option 1, talk to the hand.  Option 2, Wow! Now I know why " +
                                "everyone says that stuff about you.  Option 3, Do you think you could star in the next Mean Girls movie?" +
                                "  Which phrase would you like to practice?  Please say its number or the pharse back now, or say repeat" +
                                " to hear them again.";
                            break;
                        case "OpOneIntent":
                            log.LogLine($"phrase 1 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "You have chosen, talk to the hand. Are you ready to practice?";
                            phrase = "talk to the hand";
                            break;
                        case "OpTwoIntent":
                            log.LogLine($"phrase 2 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "You have chosen, Wow! Now I know why " +
                                "everyone says that stuff about you. Are you ready to practice?";
                            phrase = "Wow! Now I know why everyone says that stuff about you.";
                            break;
                        case "OpThreeIntent":
                            log.LogLine($"phrase 3 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "You have chosen, Do you think you could star in the next" +
                                " Mean Girls movie?. Are you ready to practice?";
                            phrase = "Do you think you could star in the next Mean Girls movie?";
                            break;
                        default:
                            log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Unknown, remember to add later";
                            break;
                    }
                }
                else if (practice1Started == true)
                {
                    switch (intentRequest.Intent.Name)
                    {
                        case "AMAZON.CancelIntent":
                            log.LogLine($"AMAZON.CancelIntent: send StopMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Goodbye!";
                            response.Response.ShouldEndSession = true;
                            break;
                        case "StopIntent":
                            log.LogLine($"AMAZON.StopIntent: send StopMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Goodbye!";
                            response.Response.ShouldEndSession = true;
                            break;
                        case "AMAZON.HelpIntent":
                            log.LogLine($"AMAZON.HelpIntent: send HelpMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "The comeback you choose was " + phrase;
                            break;
                        case "OpOneIntent":
                            log.LogLine($"phrase 1 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Good job. Let's try it again.  Remember" +
                                " to speak calmly with a hint of sarcasm. ,beep, ,beep, ,beep, Hey, dummy";
                            practice1Started = false;
                            practice2Started = true;
                            break;
                        case "OpTwoIntent":
                            log.LogLine($"phrase 2 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Good job. Let's try it again.  Remember" +
                                " to speak calmly with a hint of sarcasm. ,beep, ,beep, ,beep, Hey, dummy";
                            practice1Started = false;
                            practice2Started = true;
                            break;
                        case "OpThreeIntent":
                            log.LogLine($"phrase 3 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Good job. Let's try it again.  Remember" +
                                " to speak calmly with a hint of sarcasm. ,beep, ,beep, ,beep, Hey, Dummy";
                            practice1Started = false;
                            practice2Started = true;
                            break;
                        default:
                            log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Good try listen to the phrase again, " + phrase;
                            break;
                    }
                }
                else if (practice2Started == true)
                {
                    switch (intentRequest.Intent.Name)
                    {
                        case "AMAZON.CancelIntent":
                            log.LogLine($"AMAZON.CancelIntent: send StopMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Goodbye!";
                            response.Response.ShouldEndSession = true;
                            break;
                        case "StopIntent":
                            log.LogLine($"AMAZON.StopIntent: send StopMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Goodbye!";
                            response.Response.ShouldEndSession = true;
                            break;
                        case "AMAZON.HelpIntent":
                            log.LogLine($"AMAZON.HelpIntent: send HelpMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "The comeback you choose was " + phrase;
                            break;
                        case "OpOneIntent":
                            log.LogLine($"phrase 1 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Great. Let's try one more time." +
                                "  Remember to make eye contact as you speak, then practice walking away when you're done" +
                                " speaking. ,beep, ,beep, ,beep, Hey, dummy";
                            practice2Started = false;
                            practice3Started = true;
                            break;
                        case "OpTwoIntent":
                            log.LogLine($"phrase 2 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Great. Let's try one more time." +
                                "  Remember to make eye contact as you speak, then practice walking away when you're done" +
                                " speaking. ,beep, ,beep, ,beep, Hey, dummy";
                            practice2Started = false;
                            practice3Started = true;
                            break;
                        case "OpThreeIntent":
                            log.LogLine($"phrase 3 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Great. Let's try one more time." +
                                "  Remember to make eye contact as you speak, then practice walking away when you're done" +
                                " speaking. ,beep, ,beep, ,beep, Hey, dummy";
                            practice2Started = false;
                            practice3Started = true;
                            break;
                        default:
                            log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Good try listen to the phrase again, " + phrase;
                            break;
                    }
                }
                else if (practice3Started == true)
                {
                    switch (intentRequest.Intent.Name)
                    {
                        case "AMAZON.CancelIntent":
                            log.LogLine($"AMAZON.CancelIntent: send StopMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Goodbye!";
                            response.Response.ShouldEndSession = true;
                            break;
                        case "StopIntent":
                            log.LogLine($"AMAZON.StopIntent: send StopMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Goodbye!";
                            response.Response.ShouldEndSession = true;
                            break;
                        case "AMAZON.HelpIntent":
                            log.LogLine($"AMAZON.HelpIntent: send HelpMessage");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "The comeback you choose was " + phrase;
                            break;
                        case "OpOneIntent":
                            log.LogLine($"phrase 1 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Nice job. Would you like to practice another" +
                                " phrase?";
                            break;
                        case "OpTwoIntent":
                            log.LogLine($"phrase 2 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Nice job. Would you like to practice another" +
                                " phrase?";
                            break;
                        case "OpThreeIntent":
                            log.LogLine($"phrase 3 choosen");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Nice job. Would you like to practice another" +
                                " phrase?";
                            break;
                        case "YesIntent":
                            log.LogLine($"said yes");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Okay just say, continue, to start over";
                            practice3Started = false;
                            break;
                        case "NoIntent":
                            log.LogLine($"said no");
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Thanks for using Comforting Comebacks. Be sure" +
                                " to play every week for new phrases and techniques.  Goodbye!";
                            practice3Started = false;
                            break;
                        default:
                            log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                            innerResponse = new PlainTextOutputSpeech();
                            (innerResponse as PlainTextOutputSpeech).Text = "Good try listen to the phrase again, " + phrase;
                            break;
                    }
                }
            }

            response.Response.OutputSpeech = innerResponse;
            response.Version = "1.0";
            log.LogLine($"Skill Response Object...");
            log.LogLine(JsonConvert.SerializeObject(response));
            return response;
        }
    }
}