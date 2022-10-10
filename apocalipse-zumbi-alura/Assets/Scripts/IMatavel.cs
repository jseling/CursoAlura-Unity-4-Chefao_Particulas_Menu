using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatavel
{
    void TomarDano(int dano, Vector3 posicao, Quaternion rotacao);
    void Morrer();
}
