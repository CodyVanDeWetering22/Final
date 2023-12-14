
import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {

    async getKeeps() {
        const res = await api.get('api/keeps')
        logger.log("this is keeps", res.data)
        const newKeeps = res.data.map(pojo => new Keep(pojo))
        AppState.keeps = newKeeps
    }

    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        logger.log(res.data)
        AppState.keeps.push(new Keep(res.data))
    }

    async getKeepById(keepId) {
        AppState.activeModal = null
        logger.log(keepId)
        const res = await api.get(`api/keeps/${keepId}`)

        logger.log(res.data)
        AppState.activeModal = new Keep(res.data)

    }
    async destroyKeep(keepId) {
        const res = await api.delete(`api/keeps/${keepId}`)
        logger.log(res.data)
    }

}
export const keepsService = new KeepsService()