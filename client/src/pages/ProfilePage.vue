<template>
    <div v-if="profile" class="container-fluid">
        <div class="row justify-content-evenly">
            <div class="col-12 text-center mt-4">
                <img class="img-fluid coverImg mb-5" :src="profile.coverImg" alt="">
                <img :src="profile.picture" alt="">
                <h1>{{ profile.name }}</h1>
            </div>
        </div>
    </div>
    <div class="row mt-5">
        <h1>Vaults</h1>
        <div v-for="vault in vaults" :key="vault.id" class="col-4 mt-5 ps-4 fs-3">
            <div v-if="vault.isPrivate == false">
                <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
                    <img class="imgCard shadow" :src="vault.img" alt="">
                </router-link>
                <p> {{ vault.name }}</p>
            </div>
        </div>

    </div>

    <div class="row mt-4 justify-content-evenly">
        <h1>Keeps</h1>
        <div v-for="keep in keeps" :key="keep.id" class="col-3">
            <div>
                <div>
                    <img class="img-fluid imgCard shadow" :src="keep.img" alt="">
                </div>
                <p class="fs-4">{{ keep.name }}</p>

                <img class="rounded-circle profile shadow" :src="keep.creator.picture" alt="">


            </div>

        </div>




    </div>
</template>











<script>











import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { useRoute } from 'vue-router';
import { profileService } from '../services/ProfileService.js'

export default {
    setup() {
        onMounted(() => {
            getProfile();
            getVaultsByProfile();
            getKeepsByProfile();
        });
        const route = useRoute();
        async function getProfile() {
            try {
                const profileId = route.params.profileId;
                logger.log("this is ur profile Id", profileId);
                await profileService.getProfile(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getVaultsByProfile() {
            try {
                const profileId = route.params.profileId;
                await profileService.getVaultsByProfile(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getKeepsByProfile() {
            try {
                const profileId = route.params.profileId;
                await profileService.getKeepsByProfile(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            profile: computed(() => AppState.profile),
            account: computed(() => AppState.account),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults)
        };
    },
};
</script>


<style lang="scss" scoped>
.imgCard {
    object-fit: cover;
    object-position: center;
    position: relative;
    width: 100%;
    height: 40vh;

}

.profile {
    position: relative;
    height: 3.5vh;
    left: 81%;
    bottom: 115px;

}

p {
    position: relative;
    bottom: 80px;
    left: 5px;
    color: #ffea00f1;
    text-shadow: 3px 3px 3px black;
}

.coverImg {
    height: 30vh;
    width: 100%;
    object-fit: cover;
}
</style>
