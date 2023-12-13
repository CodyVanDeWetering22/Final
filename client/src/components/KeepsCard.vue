<template>
    <div>
        <div>
            <img @click="getKeepById(keepProp.id)" class="img-fluid imgCard shadow p-0" :src="keepProp.img" alt=""
                type="button" data-bs-toggle="modal" data-bs-target="#ActiveKeep">
            <ActiveKeepModal />
        </div>
        <p class="fs-4">{{ keepProp.name }}</p>

        <router-link :to="{ name: 'ProfilePage', params: { profileId: keepProp.creator.id } }">
            <img class="rounded-circle profile shadow" :src="keepProp.creator.picture" alt="">
        </router-link>
    </div>
</template>


<script>

import { Modal } from 'bootstrap';
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import ActiveKeepModal from './ActiveKeepModal.vue';
import { keepsService } from '../services/KeepsService.js';


export default {
    props: {
        keepProp: { type: Keep, required: true }
    },
    setup() {
        return {
            async getKeepById(keepId) {
                try {
                    await keepsService.getKeepById(keepId)
                    Modal.getOrCreateInstance(`#ActiveKeep`).hide()
                } catch (error) {
                    Pop.error(error)
                }


            }

        }
    },
    components: { ActiveKeepModal, ActiveKeepModal },
}
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
    bottom: 70px;
    left: 5px;
    color: #ffea00f1;
    text-shadow: 3px 3px 3px black;
}
</style>