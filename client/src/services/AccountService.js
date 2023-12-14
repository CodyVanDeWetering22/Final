import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async editAccount(accountData) {
    logger.log(accountData)
    const res = await api.put('/account', accountData)
    logger.log("updating?", res.data)
    AppState.account = new Account(res.data)
  }
  async getMyVaults() {
    // TODO implement this
    // SAVE the response to my Vaults
    console.log('You need to go get your own vaults')

  }



  async getMyKeeps() {

  }

}

export const accountService = new AccountService()
