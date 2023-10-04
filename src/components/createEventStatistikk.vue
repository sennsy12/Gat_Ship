<template>
    <v-card v-bind:style=cardColor elevation="5">
        <v-card-title>Antall timer hittil denne uka</v-card-title>
        <v-card-text>{{ totalt }}</v-card-text>
        <v-card-title>Min - max antall timer for denne ansatte</v-card-title>
        <v-card-text>{{ valgtAnsatt.minTimerIUka + "-" + valgtAnsatt.maxTimerIUka }}</v-card-text>
        <v-alert v-if="mindreTimerBoolean" dense type="error">Den ansatte har for få timer denne uka</v-alert>
        <v-alert v-if="mangeTimerBoolean" dense type="error">Den ansatte har for mange timer denne uka</v-alert>
        <v-alert v-if="annenTimeBoolean" dense type="warning">Denne ansatte har allerede ett annet skift denne dagen</v-alert>
        <v-alert v-if="samtidigTimeBoolean" dense type="error">Denne ansatte har et annet skift samtidig som dette</v-alert>
    </v-card>
</template>

<script>
import axios from 'axios';

export default {
    data: () => ({
        ts:0,
        cardColor: {
          backgroundColor: "",
        },
        totalt: 0.00,
        mindreTimerBoolean: false,
        mangeTimerBoolean: false,
        annenTimeBoolean: false,
        samtidigTimeBoolean: false,

        axiosSkift: []
    }),
    props: {
        valgtAnsatt: Object,
        nyttSkift: Object
        
    },
    methods: {
        kalkulerNyttSkift(time1, time2){
  // split the time strings into hours and minutes
  let [hour1, minute1] = time1.split(":");
  let [hour2, minute2] = time2.split(":");

  // calculate the hour difference
  let hourDiff = Math.abs(hour2 - hour1);
  let minuteDiff = Math.abs(minute2 - minute1) / 60;
  let diff = hourDiff + minuteDiff;

  // adjust the hour difference if necessary
  if (minute2 < minute1) {
    diff--;
  }
  return diff.toFixed(2);
        },

        skjekkeSkift(){
        axios.get("Skift/ansatt/" + this.valgtAnsatt.Ansatt_ansattNr)
      .then(response => {
        this.axiosSkift = response.data;
        console.log(this.axiosSkift);
   
      })
      .catch(error => {
        console.log(error);
      });
    },

    },



    watch: {
        nyttSkift(){
            console.log(this.nyttSkift.startTime);
            console.log(this.nyttSkift.endTime);
            if (this.nyttSkift.startTime && this.nyttSkift.endTime){
                this.ts = this.kalkulerNyttSkift(this.nyttSkift.startTime, this.nyttSkift.endTime);
            }
        },
        ts(){
            this.totalt = parseFloat(this.ts) + parseFloat(this.valgtAnsatt.antallTimerJobbet);
        },
        valgtAnsatt(){
            this.totalt = parseFloat(this.ts) + parseFloat(this.valgtAnsatt.antallTimerJobbet);

            if (this.totalt < this.valgtAnsatt.minTimerIUka) {
                this.mindreTimerBoolean = true;
                } else {
                this.mindreTimerBoolean = false;
                }

            if (this.totalt > this.valgtAnsatt.maxTimerIUka){
                this.mangeTimerBoolean = true;
            } else {
                this.mangeTimerBoolean = false;
            }

            axios.get("Skift/ansatt/" + this.valgtAnsatt.ansatt_ansattNr)
      .then(response => {
        this.axiosSkift = response.data;
   
      })
      .catch(error => {
        console.log(error);
      });

        },
        
        totalt(){
            if (this.totalt < this.valgtAnsatt.minTimerIUka) {
                this.mindreTimerBoolean = true;
                } else {
                this.mindreTimerBoolean = false;
                }

            if (this.totalt > this.valgtAnsatt.maxTimerIUka){
                this.mangeTimerBoolean = true;
            } else {
                this.mangeTimerBoolean = false;
            }
            
        },
        axiosSkift() {
            this.axiosSkift.forEach((skift)=>{
                var string = "" + skift.start;
             var subStr = string.substring(0,10);
            var count = 0;
                if (subStr == this.nyttSkift.startDate){
                    count++;
                }
            if (count > 0){
                this.annenTimeBoolean = true;
            }
            else {
                this.annenTimeBoolean = false;
            }
            });
        },
        
    }
        
    }

</script>