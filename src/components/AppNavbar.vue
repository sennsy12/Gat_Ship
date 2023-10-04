<template>
  <nav>
    <v-app-bar color="#fd9800" dark app flat>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title class="text-uppercase">
        <span class="font-weight-light">Gat</span>
        <span>Ship</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-menu offset-y>
        <template v-slot:activator="{ on }">
          <v-btn text v-on="on">
            <v-icon left>expand_more</v-icon>
            <span>Menu</span>
          </v-btn>
        </template>
        <v-list flat>
          <v-list-item
            v-for="link in links"
            :key="link.text"
            router
            :to="link.route"
            active-class="border"
          >
            <v-list-item-title>{{ link.text }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
      <div v-if="!store.token">
        <v-btn text>
          <span>
            <router-link
              style="text-decoration: none !important; color: white"
              to="/"
              >logIn</router-link
            ></span
          >
          <v-icon right>person</v-icon>
        </v-btn>
      </div>
      <div v-if="store.token">
        <v-btn text>
          <span>
            <a
              href="javascript:void(0)"
              @click="store.logout"
              style="text-decoration: none !important; color: white"
              >logout</a
            ></span
          >
        </v-btn>
      </div>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" dark app class="grey darken-4">
      <v-layout column align-center>
        <v-flex class="mt-5">
          <transition>
            <v-avatar size="100" v-show="drawer">
              <img src="../assets/gatship_logo.svg" alt="" /> </v-avatar
          ></transition>
          <!-- <p class="white--text subheading mt-1 text-center">Gatship</p> -->
        </v-flex>
        <v-flex class="mt-4 mb-4"> </v-flex>
      </v-layout>
      <v-list flat>
        <v-list-item
          v-for="link in links"
          :key="link.text"
          router
          :to="link.route"
          active-class="border"
        >
          <v-list-item-action>
            <v-icon>{{ link.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>{{ link.text }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
  </nav>
</template>
<script>
import { useAuthStore } from '@/store/auth';


export default {
  name: "AppNavbar",

  setup() {
    const store = useAuthStore();

    return { store };
  },

  data: () => ({
   
    drawer: true,
   
  
    links: [
      { icon: "dashboard", text: "Dashboard", route: "/dashboard" },
      { icon: "calendar_month", text: "Calendar", route: "/calendar" },
      { icon: "person", text: "Profile", route: "/profileView" },
    ],
    

  }),

  methods: {
    //  handleClick(){
    //   localStorage.removeItem('token');
    //   this.$router.push('/');
    //  }
  },
  watch:{

    user(){
      // console.log("this is user from watch",this.user);

    }
  },

  mounted(){
    //this.user = this.store.authUser,
  //  console.log(this.store)
  },
  
   updated(){
    // this.user = this.store.authUser
 }


};
</script>
<style scoped>
.border {
  border-left: 4px solid #fd9800;
}

.v-enter-active {
  animation: bounce-in-right 2s ease infinite;
}

.v-leave-active {
  animation: bounceIn 2s reverse;
}
@keyframes bounce-in-right {
  0% {
    opacity: 0;
    transform: translateX(2000px);
  }
  60% {
    opacity: 1;
    transform: translateX(-30px);
  }
  80% {
    transform: translateX(10px);
  }
  100% {
    transform: translateX(0);
  }
}
</style>
