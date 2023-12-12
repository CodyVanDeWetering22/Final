<template>
  <nav class="navbar navbar-expand-sm navbar-light bg-light px-3 shadow">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo"
          src="data:image/png;base64,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"
          height="45" />
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <div>
        <button class="btn text-light" @click="toggleTheme"><i class="mdi"
            :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i></button>
      </div>
      <Login />
    </div>
  </nav>
</template>

<script>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
