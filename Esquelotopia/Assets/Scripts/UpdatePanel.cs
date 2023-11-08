using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


    public class UpdatePanel : MonoBehaviour
    {
        public Personagem personagem;
        private GameObject blockText;
        private TextMeshProUGUI ataqueText;
         private TextMeshProUGUI levelText;
          private TextMeshProUGUI defesaText;
           private TextMeshProUGUI magiaText;
           private TextMeshProUGUI proxLevelText;

           public static bool atualizar;

        // Start is called before the first frame update
        void Start()
        {
            //personagem = GameObject.Find("PersonagemStats").GetComponent<Personagem>();
            blockText = GameObject.Find("BlockText");
            ataqueText = blockText.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            defesaText = blockText.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            levelText = blockText.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            magiaText = blockText.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            proxLevelText = blockText.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            atualizar = true;
        }

        // Update is called once per frame
        void Update()
        {
            
                levelText.text = "- Lvl: " + Personagem.level;
                proxLevelText.text = "( "+Personagem.proxLevelXp +"xp / 100xp )";
                ataqueText.text = "- Ataque: " + Personagem.ataque;
                defesaText.text = "- Defesa: " + Personagem.defesa;
                magiaText.text = "- Magia: " + Personagem.magia;
          

        }
    }

