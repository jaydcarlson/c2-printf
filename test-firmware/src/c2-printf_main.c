//=========================================================
// src/c2-printf_main.c: generated by Hardware Configurator
//
// This file will be updated when saving a document.
// leave the sections inside the "$[...]" comment tags alone
// or they will be overwritten!!
//=========================================================

//-----------------------------------------------------------------------------
// Includes
//-----------------------------------------------------------------------------
#include <SI_EFM8BB1_Register_Enums.h>                  // SFR declarations
#include "InitDevice.h"
#include <stdio.h>
// $[Generated Includes]
// [Generated Includes]$

//-----------------------------------------------------------------------------
// SiLabs_Startup() Routine
// ----------------------------------------------------------------------------
// This function is called immediately after reset, before the initialization
// code is run in SILABS_STARTUP.A51 (which runs before main() ). This is a
// useful place to disable the watchdog timer, which is enable by default
// and may trigger before main() in some instances.
//-----------------------------------------------------------------------------
void SiLabs_Startup (void)
{
  // Call hardware initialization routine
}

uint8_t xdata printData[255]   _at_ 0x000;   /* array at xdata 0x000 */

//-----------------------------------------------------------------------------
// main() Routine
// ----------------------------------------------------------------------------
int main (void)
{
	int i;
	enter_DefaultMode_from_RESET();

	for(i = 0;i<sizeof(printData); i++)
	{
	  printData[i] = 0;
	}
	while (1)
	{
	   i = 0;
	   while(1) {
		   i++;
		  sprintf(printData, "Hello, world #%u!\n", i); // print "Hello, world #0 (etc)
		  printData[254] = 1; // tell the computer software we're ready to read
		  while(printData[254] == 1); // wait for the computer software to reset our flag
	   }
	}
}