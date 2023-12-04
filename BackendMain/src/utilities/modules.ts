import Safety from "./safety.js";

class Modules {

    public async getModules(loopKey: string): Promise<string[] | boolean> {

        /*const modules = await fetch("http://asteria.nexusfn.net/v2/loopkey/modules", {
            method: 'GET',
            headers: {
                "loopkey": loopKey
            }
        }).then(res => res.json());

        if (modules.status !== "ok") {
            //console.log(modules);
            Safety.registerLoopKey();
            return false;
        }
        if (!modules) return [];

        const modulesJSON = modules;
        */
        return ["shop", "matchmaker"];
    }

    public async configureModules(modules: string[]) {

        try {
            Safety.modules.Shop = modules.includes("shop");
            Safety.modules.Matchmaking = modules.includes("matchmaker");
        } catch(error) {
        }

    }

}

export default new Modules();