using UnityEngine;
using System.Collections;

public class PerlinNoise {
	
	long seed;
	
	public PerlinNoise(long seed){
		this.seed = seed;
	}
	
	private int random(long x, int range){
		return (int)(((x+seed)^5) % range);
	}
	
	public int getNoise(int x, int range){
		int chunkSize = 16;
		
		float noise = 0;
		
		range /= 2;
		
		while(chunkSize > 0){
			int chunkIndex = x / chunkSize;
			
			float prog = (x % chunkSize) / (chunkSize * 1f);
			
			float left_random = random(chunkIndex, range);
			float right_random = random(chunkIndex + 1, range);
			
			
			noise += (1-prog) * left_random + prog * right_random;
			
			chunkSize /= 2;
			range /= 2;
			
			range = Mathf.Max(1,range);
		}
		
		return (int)Mathf.Round(noise);
	}
}
