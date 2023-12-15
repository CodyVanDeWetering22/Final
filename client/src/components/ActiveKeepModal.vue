<template>
    <div class="modal fade" id="ActiveKeep" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div v-if="activeKeep" class="modal-body container">
                    <div class="row">

                        <div class="col-12 d-flex justify-content-between">
                            <div>
                                <i class="mdi mdi-eye">{{ activeKeep.views }}</i>
                                <i class=" mdi mdi-plus">{{ activeKeep.kept }}</i>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                    </div>


                    <div class="row text-end mt-3">
                        <div v-if="account.id = activeKeep.creatorId">
                            <button @click="destroyKeep()" class="btn btn-warning text-end mb-4" title="delete"><i
                                    class="mdi mdi-delete"></i></button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 d-flex align-items-center text-center">

                            <div v-if="activeKeep" class="col-4">
                                <div class="d-flex">
                                    <div class="p-0">
                                        <img class="bigImg img-fluid" :src="activeKeep.img" alt="">
                                    </div>
                                </div>
                            </div>

                            <div v-if="activeKeep" class="col-8">
                                <div class="fs-4">
                                    <div class="d-inline">
                                        <h1> {{ activeKeep.name }}</h1>
                                    </div>

                                    {{ activeKeep.description }}
                                </div>
                            </div>


                        </div>
                    </div>

                    <div v-if="activeKeep" class="text-end">
                        <form @submit.prevent="createVaultKeep(activeKeep.id)">
                            <select v-model="editable.vaultId" class="form-select" aria-label="Default select example">
                                <option :value="vault.id" v-for="vault in myVaults" :key="vaultId">{{ vault.name }}
                                </option>
                            </select>
                            <button type="submit">Submit</button>
                        </form>

                        <img class="rounded-circle profile ms-5" :src="activeKeep.creator?.picture" alt="">

                    </div>

                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';
import { Modal } from 'bootstrap';
import { logger } from '../utils/Logger.js';
import ProfilePage from '../pages/ProfilePage.vue';
import { vaultKeepsService } from '../services/VaultKeepsService.js';

export default {
    setup() {

        const editable = ref({})

        return {
            editable,
            activeKeep: computed(() => AppState.activeModal),
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile),
            myVaults: computed(() => AppState.myVaults),


            async destroyKeep() {
                try {
                    const yes = await Pop.confirm(
                        "are you sure you want to delete?"
                    )
                    if (!yes) {
                        return
                    }
                    const keepId = AppState.activeModal.id
                    logger.log(`this is the delete id ${keepId}`)
                    await keepsService.destroyKeep(keepId)
                } catch (error) {
                    Pop.error(error)
                }
            },

            async createVaultKeep(keepId) {
                try {
                    const vaultKeepData = editable.value
                    vaultKeepData.keepId = keepId
                    logger.log('this is the keepid ', vaultKeepData)
                    await vaultKeepsService.createVaultKeep(vaultKeepData)
                    Modal.getOrCreateInstance('#ActiveKeep').hide()
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.bigImg {
    height: 75vh;
    width: 30vw;
    object-fit: cover;
    border-radius: 2;
}

.profile {
    height: 5vh;
}
</style>