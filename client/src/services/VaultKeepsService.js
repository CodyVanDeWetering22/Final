import { AppState } from "../AppState.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
    // TODO add keep to vault

    async getKeepsByVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log(res.data)
        AppState.vaultKeep = res.data.map(pojo => new VaultKeep(pojo))
        logger.log("this is all the keeps", AppState.vaultKeep)

    }
    // TODO get keeps in vault
    async createVaultKeep(vaultKeepData) {
        const res = await api.post('api/vaultkeeps', vaultKeepData)
        logger.log(res.data)
    }
    // TODO remove a keep from the vault
    async deleteKeepFromVault(vaultKeepId) {
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        logger.log("deleted vaultkeep", res.data)
    }
}




export const vaultKeepsService = new VaultKeepsService()