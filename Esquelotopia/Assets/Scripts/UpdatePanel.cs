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
            if(atualizar){
                levelText.text = "- Lvl: " + personagem.level;
                proxLevelText.text = "( "+personagem.proxLevelXp +"xp / 100xp )";
                ataqueText.text = "- Ataque: " + personagem.ataque;
                defesaText.text = "- Defesa: " + personagem.defesa;
                magiaText.text = "- Magia: " + personagem.magia;
                atualizar = false;
            }
          

        }
    }

