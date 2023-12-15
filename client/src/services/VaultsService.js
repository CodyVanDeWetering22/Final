import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js"

class VaultsService {
    async getVault(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}`)
        console.log("this is the vault", res.data);
        AppState.activeVault = new Vault(res.data)
        logger.log(`vault in appstate`, AppState.activeVault)
    }

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        logger.log('cool beans', res.data)
        AppState.myVaults.push(new Vault(res.data))
    }

    async destroyVault(vaultId) {
        const res = await api.delete(`api/vaults/${vaultId}`)
        logger.log("deleted", res.data)
    }


}


export const vaultsService = new VaultsService()