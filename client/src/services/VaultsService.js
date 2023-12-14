import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js"

class VaultsService {
    async getVault(vaultId) {
        AppState.activeVault = null
        const res = await api.get(`api/vaults/${vaultId}`)
        console.log("this is the vault", res.data);
        AppState.activeVault = new Vault(pojo => new Vault(pojo))
    }

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        logger.log('cool beans', res.data)
        AppState.myVaults.push(new Vault(res.data))
    }


    // TODO add keep to vault
    // TODO get keeps in vault
    // TODO remove a keep from the vault


}


export const vaultsService = new VaultsService()