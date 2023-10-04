<template>
    <v-footer
      dark
      padless
      app
      inset
      
    >
      <v-card
        class="flex"
        flat
        tile
        color="#fd9800"
        :elevation="10"
      >
  
        <v-card-text class="py-2 white--text text-center">
          {{ new Date().getFullYear() }} — <strong>GatShip</strong>
        </v-card-text>
      </v-card>
    </v-footer>
  </template>