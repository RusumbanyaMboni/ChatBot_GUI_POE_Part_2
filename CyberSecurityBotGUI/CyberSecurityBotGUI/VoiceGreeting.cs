using System;
using NAudio.Wave;

namespace CyberSecurityBotGUI
{
    class VoiceGreeting
    {
        public static void PlayGreeting()
        {
            try
            {
                using (var audioFile = new AudioFileReader("welcome.wav"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Audio greeting could not be played: " + ex.Message);
            }
        }
    }
}