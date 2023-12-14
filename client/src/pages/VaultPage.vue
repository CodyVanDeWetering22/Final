<template>
    <div class="container-fluid">

        <div class="row">
            {{ activeVault }}
        </div>

        <div class="row">
            <div v-for="keep in keeps" :key="keep.id" class="col-12">

                <img :src="keep.img" alt="">
            </div>

        </div>
    </div>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { vaultsService } from '../services/VaultsService.js'

export default {
    setup() {
        onMounted(() => {
            getVault()
            // get the keeps for this vault
        })
        const route = useRoute()


        // TODO get the keeps for this vault


        async function getVault() {
            try {
                const vaultId = route.params.vaultId
                logger.log("this is the vault id", vaultId)
                await vaultsService.getVault(vaultId)
            } catch (error) {
                Pop.error(error)
            }
        }
        return {

            keeps: computed(() => AppState.keeps),
            activeVault: computed(() => AppState.activeVault)
        }
    }
};
</script>


<style lang="scss" scoped></style>