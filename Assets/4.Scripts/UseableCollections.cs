using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Isometric.Utility;
namespace Isometric
{
    public class UseableCollections : SingletonDontDestroyMonobehavior<UseableCollections>
    {
        [SerializeField] private Spell[] spells;
        
        private void Start()
        {
            
        }
        public Spell CastSpell(string spellName)
        {
            Spell spell = Array.Find(spells, x => x.SpellName == spellName);
            return spell;
        }

        public Spell GetSpell(string spellName)
        {
            Spell spell = Array.Find(spells, x => x.SpellName == spellName);
            return spell;
        }
    }

}