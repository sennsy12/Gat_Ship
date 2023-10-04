<template>
  <v-app class="grey lighten-4">
 
    
    <AppFooter/>

    <v-main class="mx-4 my-4">
      <router-view/>
    </v-main>
   
    
  </v-app>
</template>

<script>
import { useAuthStore } from '@/store/auth';

import AppFooter from './components/AppFooter.vue';

export default {
  components: {  AppFooter},
  name: 'App',
  setup() {
    const store = useAuthStore();

    return { store };
  },
 
}
</script>


