import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:kutuphane_flutter_app/Model/notification_model.dart';
import 'dart:convert';
import 'package:kutuphane_flutter_app/variables.dart';

class NotificationPage extends StatefulWidget {
  const NotificationPage({Key? key}) : super(key: key);

  @override
  _NotificationPageState createState() => _NotificationPageState();
}

class _NotificationPageState extends State<NotificationPage> {
  List<NotificationModel> bildirimler = [];

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    getNotifications();
  }

  @override
  Widget build(BuildContext context) => Scaffold(
    appBar: AppBar(
      backgroundColor: Color(0xff176C82),
      title: Text("Bildirimler"),
    ),
    body: Center(
      child: Column(
        children: <Widget>[
          SizedBox(height: 8,),
          Expanded(
            child: ListView.builder(
              itemCount: bildirimler.length,
              itemBuilder: (BuildContext context, int index){
                final bildirim = bildirimler[index];
                return buildNotification(bildirim, index);
              },
            ),
          ),
        ],
      ),
    ),
  );

  Future getNotifications() async {
    List<NotificationModel> notifs = [];
    var response = await http
        .get(Uri.parse('http://10.0.2.2:44383/api/Notification/$loginId'));
    if (response.statusCode == 200) {
      var jsonresponse = json.decode(response.body);
      for (var n in jsonresponse) {
        NotificationModel nm = NotificationModel.fromJson(n);
        notifs.add(nm);
      }
    }
    setState(() {
      this.bildirimler = notifs;
    });
  }

  Future deleteNotification(int notificationId) async{
    var response = await http.delete(Uri.parse('http://10.0.2.2:44383/api/Notification/$notificationId'));
    if(response.statusCode == 200){
      setState(() {
        getNotifications();
      });
    }
  }

  Widget buildNotification(NotificationModel notification, int index) =>
      Container(
        decoration: const BoxDecoration(
          border: Border(bottom: BorderSide(width: .5, color: Colors.black26))),
        child: ListTile(
          //leading: Text(index.toString(),style: TextStyle(color: Colors.black54),),
          title: Text(notification.Bildirim),
          trailing: IconButton(
            iconSize: 48,
            color: Colors.white,
            onPressed: () {deleteNotification(notification.Id);},
            icon: Icon(Icons.delete),
          ),
        ),
      );
}
