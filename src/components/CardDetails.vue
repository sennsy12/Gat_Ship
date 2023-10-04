<template>
    <v-dialog v-model="dialog" persistent max-width="600px">
      <v-card>
        <v-card-title>{{ title }}</v-card-title> // legge til flere slots basert på det vi trenger
        <v-card-text>{{ content }}</v-card-text>
        <v-card-actions>
          <v-btn color="primary" text @click="dialog = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </template>
  
  <script>
  export default {
    props: {
      title: {
        type: String, // adde data vi trenger her
        default: ''
      },
      content: {
        type: String, // adde data vi trenger her
        default: ''
      },
      open: Boolean
    },

    watch: {
      open(){
        this.dialog = !this.dialog;
      }
    },
    data () {
      return {
        dialog: false
      }
    }
  }
  </script>