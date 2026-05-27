using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;

namespace CyberSecurityBotGUI
{
    class ResponseHandler
    {
        static Random random = new Random();

        static string lastTopic = "";
        static string favouriteTopic = "";

        delegate string BotReply(string input, string name);

        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            {
                "password", new List<string>
                {
                    "Use strong, unique passwords for each account. Avoid using your name, birthday, or simple words.",
                    "A strong password should include uppercase letters, lowercase letters, numbers, and symbols.",
                    "Never reuse the same password on many websites. If one account is hacked, others may be at risk."
                }
            },
            {
                "phishing", new List<string>
                {
                    "Phishing is when attackers trick you into giving personal information through fake emails or websites.",
                    "Always check the sender's email address before clicking links.",
                    "Do not enter your password on suspicious links sent by email or SMS."
                }
            },
            {
                "scam", new List<string>
                {
                    "Online scams often create urgency. Always stop and verify before sending money or personal details.",
                    "Be careful of messages saying you won a prize or must pay immediately.",
                    "If something sounds too good to be true, it is probably a scam."
                }
            },
            {
                "privacy", new List<string>
                {
                    "Privacy means protecting your personal information from people who should not access it.",
                    "Review your account privacy settings regularly, especially on social media.",
                    "Avoid sharing your ID number, address, banking details, or passwords online."
                }
            },
            {
                "malware", new List<string>
                {
                    "Malware is harmful software designed to damage, steal, or spy on your device.",
                    "Avoid downloading files from unknown websites because they may contain malware.",
                    "Keep your antivirus and operating system updated to reduce malware risks."
                }
            },
            {
                "firewall", new List<string>
                {
                    "A firewall helps block unauthorised access to your computer or network.",
                    "Firewalls monitor incoming and outgoing network traffic.",
                    "A firewall is an important security layer, but it should be used with antivirus software too."
                }

            },
            {
                "cybersecurity", new List<string>
                {
                    "Cybersecurity is the practice of protecting systems, devices, and data from digital attacks.",
                    "Good cybersecurity habits include using strong passwords, keeping software updated, and being cautious online.",
                    "Staying informed about the latest cybersecurity threats can help you stay safe online."
                }
            },
            {
                "online safety", new List<string>
                {
                    "Online safety means protecting yourself and your information while using the internet.",
                    "Be cautious about sharing personal information online, especially on social media.",
                    "Use privacy settings and be mindful of who can see your posts and information."
                }
            },
            {
                "default", new List<string>
                {
                    "I can help you with cybersecurity topics like passwords, phishing, scams, privacy, malware, and firewalls.",
                    "Feel free to ask me about any cybersecurity topic you're interested in!",
                    "Remember, staying safe online is important. Let me know if you have any questions about cybersecurity."
                }
            }

        };

        public static string GetResponse(string input, string name)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type something so I can help you.";

            input = input.ToLower();

            BotReply replyMethod = GenerateReply;
            return replyMethod(input, name);
        }
         // Conversation with Chat
        private static string GenerateReply(string input, string name)
        {
            string sentimentMessage = DetectSentiment(input, name);

            if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey"))
            {
                return $"Hello {name}! How can I help you stay safe online today?";
            }

            if (input.Contains("how are you ") || input.Contains("how are you doing") || input.Contains("are you okay"))
            {
                return $"i am a chatbot i cannot feel any emotion but thank you for asking {name}. What would you like to know about Cybersecurity today? ";
            }

            if (input.Contains("thank you") || input.Contains("thanks"))
            {
                return $"You're welcome {name}! Stay safe online.";
            }

            if (input.Contains("what is my name") || input.Contains("what's my name"))
            {
                return $"Your name is {name}.";
            }

            if (input.Contains("purpose"))
            {
                return $"My purpose is to help you understand and learn about Cybersecurity. What would like to learn about cybersecurity today?";
            }

            if (input.Contains("i'm interested in") || input.Contains("i am interested in"))
            {
                favouriteTopic = input.Replace("i'm interested in", "").Replace("i am interested in", "").Trim();
                lastTopic = favouriteTopic;

                return $"Great {name}! I will remember that you are interested in {favouriteTopic}. It is an important part of staying safe online.";
            }

            if (input.Contains("my favourite topic is") || input.Contains("my favorite topic is"))
            {
                favouriteTopic = input.Replace("my favourite topic is", "").Replace("my favorite topic is", "").Trim();
                lastTopic = favouriteTopic;

                return $"Thanks {name}. I will remember that your favourite cybersecurity topic is {favouriteTopic}.";
            }

            if (input.Contains("what is my favourite topic") || input.Contains("what is my favorite topic"))
            {
                if (!string.IsNullOrWhiteSpace(favouriteTopic))
                    return $"Your favourite cybersecurity topic is {favouriteTopic}.";

                return "You have not told me your favourite cybersecurity topic yet.";
            }

            if (input.Contains("another tip") || input.Contains("give me another") || input.Contains("tell me more") || input.Contains("explain more"))
            {
                if (!string.IsNullOrWhiteSpace(lastTopic) && keywordResponses.ContainsKey(lastTopic))
                {
                    return GetRandom(keywordResponses[lastTopic]);
                }

                if (!string.IsNullOrWhiteSpace(favouriteTopic))
                {
                    return $"Since you are interested in {favouriteTopic}, remember to keep learning and review your security settings regularly.";
                }

                return "Sure. Please tell me which topic you want to learn more about, for example password, phishing, scam, privacy, or malware.";
            }

            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    lastTopic = keyword;
                    string response = GetRandom(keywordResponses[keyword]);

                    if (!string.IsNullOrWhiteSpace(sentimentMessage))
                        return sentimentMessage + " " + response;

                    return response;
                }
            }

            if (input.Contains("cybersecurity") || input.Contains("online safety"))
            {
                return "Cybersecurity is the practice of protecting systems, devices, and data from digital attacks.";
            }

            if (!string.IsNullOrWhiteSpace(sentimentMessage))
            {
                return sentimentMessage + " You can ask me about passwords, phishing, scams, privacy, malware, or firewalls.";
            }

            return $"I'm not sure I understand {name}. Can you try rephrasing? You can ask me about passwords, scams, phishing, privacy, malware, or firewalls.";
        }

        private static string DetectSentiment(string input, string name)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid"))
            {
                return $"It's completely understandable to feel worried {name}. Cybersecurity can be confusing, but I will help you step by step.";
            }

            if (input.Contains("frustrated") || input.Contains("angry") || input.Contains("annoyed"))
            {
                return $"I understand your frustration {name}. Let us take it slowly and solve it together.";
            }

            if (input.Contains("curious") || input.Contains("interested"))
            {
                return $"That's great {name}! Being curious is a good way to learn cybersecurity.";
            }

            if (input.Contains("confused") || input.Contains("don't understand") || input.Contains("not understand"))
            {
                return $"No problem {name}. I can explain it in a simpler way.";
            }

            return "";
        }

        private static string GetRandom(List<string> responses)
        {
            return responses[random.Next(responses.Count)];
        }
    }
}
