<template>
    <v-dialog v-model="dialog" max-width="600px">
      <v-card elevation="5">
        <v-card-title>Slots that are not filled in week {{ uketall }}</v-card-title>
        <div v-for="(shift, index) in shift" :key="index">
            <v-card-text>{{ dager[shift.startDay] }} - {{ shift.start.substring(11, 16) }} to {{ dager[shift.endDay] }} - {{ shift.end.substring(11, 16) }} </v-card-text>
        </div>
        <v-card-actions>
          <v-btn color="primary" text @click="dialog = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </template>
  
  <script>
  import axios from 'axios';
  export default {
    props: {
        uketall: Number,
        open: Boolean,
        kontorNrStore: Number,
        shift: Array
    },

    methods: {
        oppdaterSkifts(){
            console.log(this.kontorNrStore)

            var link = "/skift/" + this.kontorNrStore + "/" + this.uketall;
        axios.get(link)
      .then(response => {
        this.shifts = response.data;
      })
      .catch(error => {
        console.log(error);
      });
        }
    },

    mounted () {
        
      },

    watch: {
      open(){
        this.dialog = !this.dialog;

        var link = "/skift/" + this.kontorNrStore + "/" + this.uketall;
        axios.get(link)
      .then(response => {
        this.shifts = response.data;
      })
      .catch(error => {
        console.log(error);
      });
        
      }
    },
    data () {
      return {
        dialog: false,
        kontorNr: 4,
        dager: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"]
      }
    }
  }
  </script>