// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'book_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

BookModel _$BookModelFromJson(Map<String, dynamic> json) => BookModel(
      json['Id'] as int,
      json['Isim'] as String,
      json['Yazar'] as String,
      json['Tur'] as String,
      json['Sayfa'] as int,
      json['Durum'] as String,
    );

Map<String, dynamic> _$BookModelToJson(BookModel instance) => <String, dynamic>{
      'Id': instance.Id,
      'Isim': instance.Isim,
      'Yazar': instance.Yazar,
      'Tur': instance.Tur,
      'Sayfa': instance.Sayfa,
      'Durum': instance.Durum,
    };
