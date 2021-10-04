/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 1;
    private float maxY = -5;

    [HideInInspector]
    public float _velocity;

    [HideInInspector]
    public float minVelocity;
    [HideInInspector]
    public float maxVelocity;
    [HideInInspector]
    public float changeVelocityPerFrame;

    private void Start()
    {
        if(maxVelocity < minVelocity)
        {
            minVelocity = minVelocity * maxVelocity; //a=50 (5*10)      
            maxVelocity = minVelocity / maxVelocity; //b=5 (50/10)      
            minVelocity = minVelocity / maxVelocity; //a=10 (50/5)    
        }

        _velocity = minVelocity;
    }


    private void Update()
    {
        _velocity = Mathf.MoveTowards(_velocity, maxVelocity, changeVelocityPerFrame);
        _velocity = Mathf.Clamp(_velocity, minVelocity, maxVelocity);
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed * _velocity);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (transform.position.y < maxY)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ShipModel")
        {
            Game.GameOver();
            Destroy(gameObject);
        }
    }
}
