import os
import json
res = {}
for root, dirs, files in os.walk("."):
    for f in files:
        path = os.path.relpath(os.path.join(root, f), ".")
        res[f] =  "require(../../assets/personagem/" + path.replace("\\", "/") + ")"

open("consts.txt",'w').write(str(json.dumps(res)))