

<template>

  <v-row class="fill-height">
   
    <v-col>
      <v-sheet height="64">
        <v-toolbar flat color="white">
          <v-btn 
       outlined class="v-btn black--text mr-4"
       elevation="4" 
       :style="{ 'background-color': hover ? 'black' : 'white' }"
       @click="setToday">Today
    </v-btn>
          <v-btn fab text small @click="prev">
            <v-icon small>mdi-chevron-left</v-icon>
          </v-btn>

          <v-btn fab text small @click="next">
            <v-icon small>mdi-chevron-right</v-icon>
          </v-btn>
          <v-btn 
       outlined class="v-btn black--text mr-4"
       elevation="4" 
       :style="{ 'background-color': hover ? 'black' : 'white' }"
       @click="capture">Picture Calendar
    </v-btn>
    <v-btn icon href="/ViewSchedule" target="_blank">
  <v-icon>schedule</v-icon> 
</v-btn>
<v-btn @click="angreMethod" v-if="slettetEventBoolean">Angre</v-btn>
          <v-toolbar-title>{{ title }}</v-toolbar-title>
          <div class="flex-grow-1">
          </div>
          <v-menu bottom right>
            <template v-slot:activator="{ on }">
              <v-btn outlined v-on="on">
                <span>{{ typeToLabel[type] }}</span>
                <v-icon right>mdi-menu-down</v-icon>
              </v-btn>
            </template>
            <v-list>
              <v-list-item @click="type = 'day'">
                <v-list-item-title>Day</v-list-item-title>
              </v-list-item>
              <v-list-item @click="type = 'week'">
                <v-list-item-title>Week</v-list-item-title>
              </v-list-item>
              <v-list-item @click="type = 'month'">
                <v-list-item-title>Month</v-list-item-title>
              </v-list-item>
              <v-list-item @click="type = '4day'">
                <v-list-item-title>4 days</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </v-toolbar>
      </v-sheet>

      
      <v-dialog v-model="dialog" max-width="500" @click:outside="handleClickOutside">
  <v-card>
    <v-container>
      <v-form @submit.prevent="addEvent">
        <v-row justify="center">
          <SelectAnsatt :kontorNr="kontorNrStore" :uketall="uketall" @valgtAnsattNr="endreAnsattNr"></SelectAnsatt>
          <createEventStatistikk v-show="ansattNr" :nytt-skift="nyttSkift" :valgt-ansatt="valgtAnsatt"></createEventStatistikk>
          <v-menu v-model="startDateMenu" :close-on-content-click="false" transition="scale-transition" offset-y min-width="290px">
            <template v-slot:activator="{ on }">
              <v-text-field v-model="startDate" label="Start Date" prepend-icon="mdi-calendar" readonly v-on="on"></v-text-field>
            </template>
            <v-date-picker v-model="startDate" @input="startDateMenu = false"></v-date-picker>
          </v-menu>
          
          <v-menu v-model="startTimeMenu" :close-on-content-click="false" transition="scale-transition" offset-y min-width="290px">
            <template v-slot:activator="{ on }">
              <v-text-field v-model="startTime" label="Start Time" prepend-icon="mdi-clock-time-three-outline" readonly v-on="on"></v-text-field>
            </template>
            <v-time-picker v-model="startTime" format="24hr" :allowed-minutes="allowedStep" @input="startTimeMenu = false"></v-time-picker>
          </v-menu>
          <v-menu v-model="endTimeMenu" :close-on-content-click="false" transition="scale-transition" offset-y min-width="290px">
            <template v-slot:activator="{ on }">
              <v-text-field v-model="endTime" label="End Time" prepend-icon="mdi-clock-time-three-outline" readonly v-on="on"></v-text-field>
            </template>
            <v-time-picker v-model="endTime" format="24hr" :allowed-minutes="allowedStep" @input="endTimeMenu = false"></v-time-picker>
          </v-menu>
        </v-row>
        <v-btn type="submit" color="primary" class="mr-4" @click.stop="dialog = false">
          Create Event
        </v-btn>
      </v-form>
    </v-container>
  </v-card>
</v-dialog>




<v-sheet height="600">
  <v-calendar
  ref="calendar"
  v-model="focus"
  color="orange"
  :events="events"
  :event-color="getEventColor"
  :event-margin-bottom="3"
  :now="today"
  :type="type"
  @click:event="showEvent"
  @click:more="viewDay"
  @click:date="setDialogDate"
  @change="updateRange"
  :weekdays=[1,2,3,4,5,6,0]
  :show-week=true
  :interval-count=24
  :interval-height=21
  :interval-format="intervalFormat"
  ></v-calendar>


  <v-menu
  v-model="selectedOpen"
  :close-on-content-click="false"
  :activator="selectedElement"
  full-width
  offset-x
  >
  <v-card color="grey lighten-4" :width="350" flat>
    <v-toolbar :color="selectedEvent.color" dark>
      <v-btn @click="deleteEvent(selectedEvent)" icon>
        <v-icon>mdi-delete</v-icon>
      </v-btn>
      <v-toolbar-title v-html="selectedEvent.ansatt_ansattNr"></v-toolbar-title>
      <div class="flex-grow-1"></div>
    </v-toolbar>

    <v-card-text>
      <form v-if="currentlyEditing !== selectedEvent.id">
        {{ selectedEvent.details }}
      </form>
      <form v-else>
        <textarea-autosize
        v-model="selectedEvent.details"
        type="text"
        style="width: 100%"
        :min-height="100"
        placeholder="add note">
      </textarea-autosize>
    </form>
  </v-card-text>

  <v-card-actions>
    <v-btn text color="secondary" @click="selectedOpen = false">
      close
    </v-btn>
    <v-btn v-if="currentlyEditing !== selectedEvent.id" text @click.prevent="editEvent(selectedEvent)"> 
      edit
    </v-btn>
    <v-btn text v-else type="submit" @click.prevent="updateEvent(selectedEvent)">
      Save
    </v-btn>
  </v-card-actions>
</v-card>
</v-menu>
</v-sheet>
</v-col>
</v-row>


</template>


  
  
  <script>
import axios from 'axios';
import html2canvas from 'html2canvas';
import SelectAnsatt from './SelectAnsatt.vue';
import createEventStatistikk from './createEventStatistikk.vue';

import jwt_decode from 'jwt-decode'

import { useAuthStore } from "@/store/auth";




    export default {
      components: {SelectAnsatt, createEventStatistikk},



      data: () => ({
        closeDialogOnNextTick: false,
        focus: '',
        uketall: 0,
        type: 'week',
        typeToLabel: {
          month: 'Month',
          week: 'Week',
          day: 'Day',
          '4day': '4 Days',
        },
        selectedEvent: {},
        selectedElement: null,
        selectedOpen: false,
        events: [],
        startDate:'',
        endDate:'',
        startTime:'',
        endTime:'', 
        ansattNr:'',

        valgtAnsatt: {},
        nyttSkift: {},

        kontorNr: 4,

        slettetEvent: {},
        slettetEventBoolean: false
       
    
      }),
      mounted () {
        this.updateCalendar()
        console.log(this.kontorNrStore);
      },

      watch: {
        startDate(){
          this.uketall = this.getUketall(this.startDate);
          this.oppdaterNyttSkift(this.startTime, this.endTime, this.startDate, this.endDate)
        },
        endDate(){
          this.oppdaterNyttSkift(this.startTime, this.endTime, this.startDate, this.endDate)

        },
        startTime(){
          this.oppdaterNyttSkift(this.startTime, this.endTime, this.startDate, this.endDate)

        },
        endTime(){
          this.oppdaterNyttSkift(this.startTime, this.endTime, this.startDate, this.endDate)
        },


      },

      setup() {
    const store = useAuthStore();
    let username = "";
    let navn = "";
    let epost = "";
    let kontorNrStore = ""
   


    if (store.token) {
const decoded = jwt_decode(store.token)
username = decoded.unique_name;
navn = decoded.navn
epost = decoded.epost
kontorNrStore = decoded.kontorNr
        }
    return { store, username, navn, epost, kontorNrStore };
  },
      
      methods:
      {
        oppdaterNyttSkift(startTid, sluttTid, startDag, sluttDag){
          var tmp = {
            startTime : startTid,
            endTime : sluttTid,
            startDate : startDag,
            endDate : sluttDag
          }
          this.nyttSkift = tmp;
        },
        allowedStep(m){
          return m % 15 === 0;
        },
        
        updateCalendar(){
          axios.get('Skift/' + this.kontorNrStore)
      .then(response => {
        this.events = response.data;
   
      })
      .catch(error => {
        console.log(error);
      });
      
      this.$refs.calendar.checkChange()
        },

        viewDay ({ date }) {
          this.focus = date
          this.type = 'day'
        },

        getUketall(date){
          var d = new Date(date);
          // Set to nearest Thursday: current date + 4 - current day number
          // Make Sunday's day number 7
          d.setUTCDate(d.getUTCDate() + 4 - (d.getUTCDay()||7));
          // Get first day of year
          var yearStart = new Date(Date.UTC(d.getUTCFullYear(),0,1));
          // Calculate full weeks to nearest Thursday
          var weekNo = Math.ceil(( ( (d - yearStart) / 86400000) + 1)/7);
          // Return week number
          console.log(weekNo);
          return weekNo;
        },
        getEventColor (event) {
          return event.color
        },
        setToday () {
          this.focus = ''
        },
        prev () {
          this.$refs.calendar.prev()
        },
        next () {
          this.$refs.calendar.next()
        },



        deleteEvent(event){
          this.slettetEvent = event;
          this.slettetEventBoolean = true;
          const newEvent ={
            name: event.name,
            ansatt_ansattNr: event.ansatt_ansattNr,
            start: event.start,
            end: event.end,
            color: "blue"
          }
          axios.post('skift/slett', newEvent)
        .then(response => {
          console.log('Event submitted successfully!', response.data);

          this.updateCalendar();
          this.selectedOpen = false;
         
        })
        .catch(error => {
          console.error('Error submitting event:', error);
        });
        },
      
        showEvent ({ nativeEvent, event }) {
          const open = () => {
            this.selectedEvent = event
            this.selectedElement = nativeEvent.target
            setTimeout(() => this.selectedOpen = true, 10)
          }
  
          if (this.selectedOpen) {
            this.selectedOpen = false
            setTimeout(open, 10)
          } else {
            open()
          }
  
          nativeEvent.stopPropagation()
        },

        rnd (a, b) {
          return Math.floor((b - a + 1) * Math.random()) + a
        },

        endreAnsattNr(i)
        {
            this.ansattNr = i.ansatt_ansattNr;
            this.valgtAnsatt = i;
            console.log(i);
        },

        closeDialog() {
          this.dialog = false;
        },

        handleClickOutside(){
          this.closeDialogOnNextTick = true;
        },

        //Metode for å åpne create event vinduet
        setDialogDate ({ date }) {
          this.startDate = date;
          if (this.closeDialogOnNextTick) {
    this.dialog = false;
    this.closeDialogOnNextTick = false;
  } else {
    this.dialog = true;
  }
        },

        angreMethod(){
          const newEvent = { 
        Ansatt_ansattNr: this.slettetEvent.ansatt_ansattNr,
        start: this.slettetEvent.start,
        end: this.slettetEvent.end,
        Kontor_kontorNr: this.kontorNrStore
      };
      console.log(newEvent);
      axios.post('/Skift', newEvent)
        .then(response => {
          console.log('Event submitted successfully!', response.data);
          this.updateCalendar();
        })
        .catch(error => {
          console.error('Error submitting event:', error);
        });

        this.slettetEventBoolean = false
        },

    // metode for submit events
        addEvent() {
      const newEvent = { 
        Ansatt_ansattNr: this.ansattNr,
        start: this.startDate + ' ' + this.startTime,
        end: this.startDate + ' ' + this.endTime,
        Kontor_kontorNr: this.kontorNrStore
      };
      axios.post('/Skift', newEvent)
        .then(response => {
          console.log('Event submitted successfully!', response.data);
          this.updateCalendar();
        })
        .catch(error => {
          console.error('Error submitting event:', error);
        });
      },
          
   
        capture() {
  html2canvas(this.$el).then(canvas => {
    const dataURL = canvas.toDataURL();
    const link = document.createElement('a');
    link.download = 'calenderx.png'; // Set the file name
    link.href = dataURL;
    link.click(); // Simulate a click on the download link
  });
},

intervalFormat(interval) {
        return interval.time
    }

      },
    }

  </script>