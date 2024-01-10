<template>
  <div class="container-fluid">
    <div v-if="account" class="dropdown text-center my-5">
      <button class="btn btn-warning dropdown-toggle" title="Create Vaults or Keeps" type="button"
        id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
        Create keeps
      </button>

      <ul class="dropdown-menu p-3" aria-labelledby="dropdownMenuButton1">


        <button type="button" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#createKeepModal">
          Create Keep
        </button>

        <button type="button" class="btn btn-warning ps-1" data-bs-toggle="modal" data-bs-target="#createVaultModal">
          Create Vault
        </button>
      </ul>
    </div>

    <div class="row">
      <div v-for="keep in keeps" :key="keep.id" class="col-6 col-md-3 my-3">
        <KeepsCard :keepProp="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js'
import { logger } from '../utils/Logger.js';
import KeepsCard from '../components/KeepsCard.vue';


export default {
  setup() {
    async function getKeeps() {
      try {
        await keepsService.getKeeps()
        logger.log('cooking')
      } catch (error) {
        Pop.error(error)
      }
    }

    onMounted(() =>
      getKeeps())
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      keepImg: computed(() => `url(${props.keepProp.img})`)
    }
  },
  components: { KeepsCard }
}
</script>

<style scoped lang="scss"></style>
