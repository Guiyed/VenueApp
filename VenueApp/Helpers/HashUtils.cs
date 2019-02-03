using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.Helpers
{
    public class HashUtils
    {
        /*

import hashlib
import random
import string

def make_salt() :
    return ''.join([random.choice(string.ascii_letters) for x in range(5)])

def make_pw_hash(password, salt= None):
    if not salt:
        salt = make_salt()
    hash = hashlib.sha256(str.encode(password + salt)).hexdigest()
    return '{0},{1}'.format(hash, salt)

def check_pw_hash(password, hash):
    salt = hash.split(',')[1]
    if make_pw_hash(password, salt) == hash:
        return True

    return False
            */
    }
}
