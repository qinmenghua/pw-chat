package logparse;

import java.util.Scanner;
import java.util.ArrayList;
import java.io.*;

public class Main {

    public static void main(String[] args) throws FileNotFoundException, IOException {
        Scanner in = new Scanner(System.in);
	String logloc;
	String sqlout;
	System.out.println("Location of world2.chat (assuming /PWServer/logservice/logs/world2.chat if none given):");
	logloc = in.nextLine().replace("\r", "").replace("\n", "");
	if(logloc.equals("")){
	    logloc = "/PWServer/logservice/logs/world2.chat";
	}
	System.out.println("Location to output sql file to (log.sql is assumed if none given):");
	sqlout = in.nextLine().replace("\r", "").replace("\n", "");
	if(sqlout.equals("")){
	    sqlout = "log.sql";
	}
	BufferedReader br = new BufferedReader(new FileReader(logloc));
	BufferedWriter bw = new BufferedWriter(new FileWriter(sqlout));
	String cline;
	String[] t;
	String date;
	String type;
	String uid;
	String chldst;
	String msg;
	String sql;
	String valq = "INSERT INTO `pwchat`.`messages` (`uid`, `type`, `chldst`, `time`, `message`) VALUES \n";
	bw.write(valq);
	bw.flush();
	int i = 0;
	while((cline = br.readLine()) != null){
	    //System.out.println(cline);
	    t = cline.split(" ");
	    date = t[0] + " " + t[1];
	    type = t[6].replace(":", "");
	    uid = t[7].substring(4);
	    chldst = t[8].substring(4);
	    msg = Base64.encodeBytes(new String(Base64.decode(t[9].substring(4)), "UnicodeLittle").getBytes("UTF-8"));
	    sql = "('"+ uid + "', '"+ type + "', '"+ chldst + "', '"+ date + "', '"+ msg + "')";
	    //System.out.println(sql);
	    bw.write(sql);
	    i++;
	    if((i % 1000) == 0){
		bw.write(";\n");
		bw.write(valq);
	    } else {
		bw.write(",");
	    }
	    bw.flush();
	}
	bw.close();
	RandomAccessFile raf = new RandomAccessFile(sqlout, "rw");
	long lcharloc = raf.length()-1;
	raf.seek(lcharloc);
	byte lchar = raf.readByte();
	//this should fix the bug with sql files that end with , (thus being invalid)
	//0x2c = ,
	if(lchar == 0x2c){
	    raf.seek(lcharloc);
	    //0x3b = ;
	    raf.write(0x3b);
	}

    }

}
