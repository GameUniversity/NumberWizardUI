using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour {

	private static NumberWizard _instance;

	public static NumberWizard Instance { get { return _instance; } }

	int _max;
	int _min;

	int _current_guess = 500;
	int _guesses = 0;
	public int _max_guesses = 10;


	public Text guess_text;
	public Text guess_num_text;
	public Text guess_max_text;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
			// if we're in the "game scene" which will be the only scene where the text attribute is set
			if (guess_text != null && _guesses == 0) {
				StartGame (1, 1001);
				NextGuess ();
			}
				
		}
	}
		
	public void GuessHigher()
	{
		_min = _current_guess;
		NextGuess ();
	}

	public void GuessLower()
	{
		_max = _current_guess;
		NextGuess ();
	}

	public void NextGuess()
	{
		// if it's the first guess ... we want a random guess.
		// after that it's binary search
		if (_guesses == 0) {
			_current_guess = Random.Range (_min, _max);
		} else {
			_current_guess = (_min + _max) / 2;
		}

		_guesses++;

		guess_num_text.text = _guesses.ToString ();
		guess_max_text.text = _max_guesses.ToString ();
		guess_text.text = _current_guess.ToString();

		if (_guesses > _max_guesses) 
		{
			SceneManager.LoadScene ("Win");
		}
	}

	public void StartGame(int min, int max)
	{
		_min = min;
		_max = max ; // to ensure we get full range
		_guesses = 0;
	}

}
