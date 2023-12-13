import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Profile } from "../models/Profile.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ProfileService {
    async getProfile(profileId) {
        AppState.profile = null
        const res = await api.get(`api/profiles/${profileId}`)
        logger.log("this is the profile", res.data)
        AppState.profile = new Profile(res.data)
    }

    async getVaultsByProfile(profileId) {
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('these are ur vaults', res.data)
        AppState.vaults = res.data.map(pojo => new Vault(pojo))
    }

    async getKeepsByProfile(profileId) {
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log("these are the keeps account", res.data)
        AppState.keeps = res.data.map(pojo => new Keep(pojo))
    }
}


export const profileService = new ProfileService()