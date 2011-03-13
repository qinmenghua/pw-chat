For source code download-------------------------------------------------------
The solution of PW-Chat contains pre and post build events that probably link
to applications you don't have on your system, GNU tools (rm, cp, mv; windows
equivs don't like me) 7zip and NSIS. You must either remove them from the build
events OR make sure you have all the tools (I like a nice automated build)

For compiled download(applies to source as well)-------------------------------
You DO NOT have to use the Setup provided, I simply have it there for ease of 
use. Inside the standalone directory is all that is needed to run PW Chat.
You must put the server files somewhere that can parse PHP; it DOES NOT
have to be on the same server as your PW Server so long as it can talk to 
gdeliveryd and gamedbd.

For any issues-----------------------------------------------------------------
I will gladly help anyone that has problems via an issue raised on Google Code
(https://code.google.com/p/pw-chat/issues/entry) or on the RaGEZONE thread
(http://forum.ragezone.com/f452/pw-chat-733110/). If you ask questions that are
just downright stupid and considered 'basic' knowledge I will not help you.

Basic Knowledge Definition-----------------------------------------------------
I consider 'basic' knowledge of operating a server as having a PW Server
already running without any issues. If you don't even have that then you do not
meet the requirements for 'basic' knowledge and I will not help you in setting
up my application due to it relying on a PW Server being present.

Bug Reporting------------------------------------------------------------------
If you DO happen to stumble across a bug or something that looks out of the
ordinary DO tell me if it still exists in the latest download. I try to update
the download on the Google Code page as soon as I fix ANY bug to keep any that
do exist to a minimum for the sake of the users and my own sanity. I attempt to
write as much bug free code as I can, but as any developer can tell you, no one
is perfect.