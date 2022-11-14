using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth{
    private int longeur;
    private int largeur;

    private int[,] grille;

    public Labyrinth(int longeur, int largeur){
        this.longeur = longeur;
        this.largeur = largeur;
        grille = new int[longeur, largeur];
    }

    public int[,] GetGrille(){
        return grille;
    }

    //fonction annexes pour la generation du labyrinth

    private void DestroyWall(int x, int y, int x2, int y2){
        //calcul de la direction (les 2 cellules sont voisines)
        int dirX = x2 - x;
        int dirY = y2 - y;
        int direction = 0;
        if(dirX == 1){
            direction = 1;
        }else if(dirX == -1){
            direction = 3;
        }else if(dirY == 1){
            direction = 2;
        }else if(dirY == -1){
            direction = 0;
        }

        //on détruit le mur
        switch(direction){
            case 0:
                grille[x, y] += 1;
                grille[x2, y2] += 4;
                break;
            case 1:
                grille[x, y] += 2;
                grille[x2, y2] += 8;
                break;
            case 2:
                grille[x, y] += 4;
                grille[x2, y2] += 1;
                break;
            case 3:
                grille[x, y] += 8;
                grille[x2, y2] += 2;
                break;
        }
    }

    private bool IsVisited(int x, int y,bool[,] visited){
        if(x < 0 || x >= longeur || y < 0 || y >= largeur){
            return true;
        }else{
            return visited[x, y];
        }
    }

    private List<int[]> GetNeighbours(int x, int y, bool[,] visited){
        List<int[]> neighbours = new List<int[]>();
        if(!IsVisited(x, y - 1, visited)){
            neighbours.Add(new int[] {x, y - 1});
        }
        if(!IsVisited(x + 1, y, visited)){
            neighbours.Add(new int[] {x + 1, y});
        }
        if(!IsVisited(x, y + 1, visited)){
            neighbours.Add(new int[] {x, y + 1});
        }
        if(!IsVisited(x - 1, y, visited)){
            neighbours.Add(new int[] {x - 1, y});
        }
        return neighbours;
    }

    public void Generate(int x, int y){
        //on initialise la pile et la grille des cases visitées
        List<int[]> pile = new List<int[]>();
        int[] current = new int[2];
        current[0] = x;
        current[1] = y;
        pile.Add(current);

        bool[,] visited = new bool[longeur, largeur];
        for(int i = 0; i < longeur; i++){
            for(int j = 0; j < largeur; j++){
                visited[i, j] = false;
            }
        }
        visited[x, y] = true;
        while(pile.Count > 0){
            //on recupère les voisins non visités
            List<int[]> neighbours = GetNeighbours(current[0], current[1], visited);
            if(neighbours.Count > 0){
                //on choisit un voisin au hasard
                int[] next = neighbours[Random.Range(0, neighbours.Count)];
                //on détruit le mur entre les 2 cellules
                DestroyWall(current[0], current[1], next[0], next[1]);
                //on ajoute la cellule courante à la pile
                pile.Add(current);
                //on se déplace sur la cellule voisine
                current = next;
                //on marque la cellule comme visitée
                visited[current[0], current[1]] = true;
            }else{
                //on revient en arrière
                current = pile[0];
                pile.RemoveAt(0);
            }
        }
    }

    private void Rotate(int x, int y){
        int value = grille[x, y];
        int last = value/8 % 2;
        value -= last*8;
        value *= 2;
        value += last;
        grille[x, y] = value;
    }

    public void RandomizeRotation(bool[,] locked){
        for(int i = 0; i < longeur; i++){
            for(int j = 0; j < largeur; j++){
                if(!locked[i, j]){
                    for(int k = 0; k < Random.Range(0, 4); k++){
                        Rotate(i, j);
                    }
                }
            }
        }
    }

}
