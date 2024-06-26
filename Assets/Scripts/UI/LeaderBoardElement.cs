using TMPro;
using UnityEngine;

namespace UI
{
    public class LeaderBoardElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _score;

        public void Initialize(string name, int rank, int score)
        {
            _name.text = name;
            _rank.text = rank.ToString();
            _score.text = score.ToString();
        }
    }
}