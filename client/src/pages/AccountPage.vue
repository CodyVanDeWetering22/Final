<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
    <img :src="account.coverImg" alt="">

    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#AccountModal">
      edit account
    </button>


    <div class="modal fade" id="AccountModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="AccountModal">Edit Account</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>

          <div class="modal-body">
            <form @submit.prevent="editAccount()">
              <div class="mb-3">
                <label for="name" class="form-label">Name</label>
                <input v-model="editable.name" type="name" class="form-control" id="name" aria-describedby="emailHelp">
              </div>

              <div class="mb-3">
                <label for="Picture" class="form-label">Picture</label>
                <input v-model="editable.picture" type="url" class="form-control" id="Picture">
              </div>

              <div class="mb-3">
                <label for="coverimg" class="form-label">Cover Image</label>
                <input v-model="editable.coverImg" type="url" class="form-control" id="coverimg">
              </div>

              <button type="submit" class="btn btn-primary">Save changes</button>
            </form>
          </div>
          <div class="modal-footer">
          </div>
        </div>
      </div>
    </div>


    <div class="container">
      <h3>My Vaults</h3>
      <div class="row">
        <div class="col-md-4">
          <div class="vault" v-for="vault in myVaults">
            <img :src="vault.img" alt="">
          </div>
        </div>
      </div>
    </div>





  </div>
</template>

<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { accountService } from '../services/AccountService.js';
import { Modal } from 'bootstrap';
export default {
  setup() {
    const editable = ref({})


    return {
      editable,
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),

      async editAccount() {
        try {
          const accountData = editable.value
          logger.log(accountData)
          await accountService.editAccount(accountData)
          Modal.getOrCreateInstance('#AccountModal').hide()

        } catch (error) {
          Pop.error(error)
        }
      }



    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>