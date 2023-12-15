<template>
    <div class="container-fluid">
        <div v-if="activeVault" class="row">
            <h1 class="center">{{ activeVault.name }}</h1>
            <img :src="activeVault.img" alt="" class="img-fluid vaultImg">
        </div>
        <div class="row">
            <div v-for="keep in vaultKeeps " :key="keep.id" class="col-12">

                <img class="vaultkeepimg mt-2" :src="keep.img" alt="">
                {{ keep.name }}
                <button @click="deleteKeepFromVault(keep.vaultKeepId)" class="mdi mdi-delete"></button>
            </div>
        </div>
    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { vaultsService } from '../services/VaultsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js';


export default {
    setup() {
        const router = useRouter()
        onMounted(() => {
            getVault()
            // get the keeps for this vault
            getKeepsByVaultId()
        })
        const route = useRoute()


        // TODO get the keeps for this vault
        async function getKeepsByVaultId() {
            try {
                const vaultId = route.params.vaultId
                logger.log(vaultId)
                await vaultKeepsService.getKeepsByVaultId(vaultId)
            } catch (error) {
                Pop.error(error)
            }
        }

        async function getVault() {
            try {
                const vaultId = route.params.vaultId
                logger.log("this is the vault id", vaultId)
                await vaultsService.getVault(vaultId)
            } catch (error) {
                Pop.error(error)
                if (error.response.data.includes('Something')) {
                    router.push({ name: 'Home' })
                }
            }
        }
        return {

            keeps: computed(() => AppState.keeps),
            activeVault: computed(() => AppState.activeVault),
            vaultKeeps: computed(() => AppState.vaultKeep),


            async deleteKeepFromVault(vaultKeepId) {
                try {

                    logger.log(vaultKeepId)
                    await vaultKeepsService.deleteKeepFromVault(vaultKeepId)
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};


</script>


<style lang="scss" scoped>
.vaultkeepimg {
    height: 30vh;
}

.vaultImg {
    height: 50vh;
    width: 100%;
    object-fit: cover;
    object-position: center;
}
</style>