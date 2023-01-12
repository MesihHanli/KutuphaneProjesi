// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'book_member_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

BookMemberModel _$BookMemberModelFromJson(Map<String, dynamic> json) =>
    BookMemberModel(
      json['BookId'] as int,
      json['BookName'] as String,
      json['Yazar'] as String,
      (json['OrtalamaPuan'] as num).toDouble(),
      json['TeslimTarihi'] as String,
    );

Map<String, dynamic> _$BookMemberModelToJson(BookMemberModel instance) =>
    <String, dynamic>{
      'BookId': instance.BookId,
      'BookName': instance.BookName,
      'Yazar': instance.Yazar,
      'OrtalamaPuan': instance.OrtalamaPuan,
      'TeslimTarihi': instance.TeslimTarihi,
    };
