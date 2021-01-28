# Assignment 1

This was a simple assignment designed to orient students with the use of AVD (Android Virtual Device) or Emulator. The general objective is to install Android Studio, start a project, get a working AVD, take screen shots where necessary, and locate the debugging apk file for sharing.

I’m running Android Studio on a mid-2012 MacBook Pro with 16GB RAM and a 500GB SSD. The AVD is pretty sluggish, so I’d like to look into increasing the response speed, but not sure if that will be possible on this machine. Luckily, I have a Samsung Galaxy S8+ I can use for testing, which should make development a bit faster.

## A note on Docker Desktop

If you’re running Docker Desktop on Mac, you definitely need to quit all Docker processes before attempting to launch the AVD. Having Docker running actually made my system virtually unresponsive. Building the project alone took nearly 2 minutes. After stopping all Docker processes the build took approx 6 seconds, and the AVD speed increased, but it’s still not in a desirable state. 
