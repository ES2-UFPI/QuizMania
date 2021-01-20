import os
import json


temp = os.walk('.', topdown=False)
for root, dirs, files in temp:
    for i in dirs:
        dir = os.path.join(root,i)
        if " " in dir:
            os.rename(dir, dir.replace(" ", ""))



res = {}
tipos = ["arm","hand", "head","hair" ,"nose", 'neck', 'shoe', 'pants', 'face', 'shirt']
for root, dirs, files in os.walk("."):
    for f in files:
        path = os.path.relpath(os.path.join(root, f), ".")
        type = "Outros"
        for tipo in tipos:
            if tipo in path.lower():
                type = tipo
        res[f] =  {"image": "require('../assets/personagem/" + path.replace("\\", "/") + "')", "tipo": type}

open("consts.txt",'w').write(str(json.dumps(res)))