<template>
    <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Create Vault</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div @submit.prevent="createVault" class="modal-body">
                    <form>
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input required v-model="editable.name" type="text" class="form-control" id="name">
                        </div>
                        <div class="mb-3">
                            <label for="description" class="form-label">description</label>
                            <input required v-model="editable.description" type="text" class="form-control"
                                id="description">
                        </div>
                        <div class="mb-3">
                            <label for="img" class="form-label">img</label>
                            <input required v-model="editable.img" type="text" class="form-control" id="img">
                        </div>
                        <div class="form-check">
                            <input v-model="editable.isPrivate" default="false" class="form-check-input" type="checkbox"
                                value="" id="flexCheckDefault">
                            <label class="form-check-label" for="flexCheckDefault">
                                Private?
                            </label>
                        </div>
                        <div class="col-10 text-end">
                            <div class="modal-footer">
                                <button type="submit" class="btn btn-warning">Submit</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { vaultsService } from '../services/VaultsService.js';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
export default {
    setup() {
        const editable = ref({})
        return {
            editable,

            async createVault() {
                try {
                    const vaultData = editable.value


                    if (vaultData.isPrivate == undefined) {
                        logger.log("is private not set")
                        vaultData.isPrivate = false
                    }
                    logger.log(vaultData)

                    await vaultsService.createVault(vaultData)
                    editable.value = {}

                    Modal.getOrCreateInstance('#createVaultModal').hide()
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>