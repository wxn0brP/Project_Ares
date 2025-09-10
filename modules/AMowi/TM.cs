namespace Ares;
using System;
using System.Speech.Synthesis;

public partial class AMowi : Form{
    SpeechSynthesizer synthesizer = new SpeechSynthesizer();
    int ktore = 0;
    
    public void Init(string plusParams, string config){
        synthesizer.SetOutputToDefaultAudioDevice();
		Application.Run(this);
    }

    public AMowi(){
        InitializeComponent();
    }
    public void Say(string a){
        Task.Run(() => {
            synthesizer.Speak(a);
        });
    }

    private void powiedzB_Click(object sender, EventArgs en){
        string text = this.TMBox.Text;
        if(text != ""){
            Say(text);
        }else{
            Say(mondrosci[ktore]);
            ktore++;
            if(ktore >= mondrosci.Length){
                ktore = 0;
            }
        }
    }

    string[] mondrosci = {
        "Szarap",
        "Towarzysz ma zawsze racje",
        "I Prawidłowo",
        "Witajcie Drodzy Aresowicze!",
        "Dobre Aresowe",
        "Gdzie dwoch się bije tam towarzysz korzysta",
        "No to idziemy Aresować",
        "Towarzysz OPCJE!",
        "Co było a nie jest nie pisze się w rejestr",
        "Zawsze mogło być gorzej",
        "Gdzie bluskreenów 6 tam dysk umarł taka smutna wieść",
        "1 bluskreen to nie koniec świata",
        "Dawaj aresa bo nie zdzierżę z tym łindołsem",
        "Oficjalna strona Projektu Ares to projektares.tk",
        "Jak Mówił Poeta Aresa się kupuje a nie piraci z neta"
    };

}