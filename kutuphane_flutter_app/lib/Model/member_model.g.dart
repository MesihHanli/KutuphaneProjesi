// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'member_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

MemberModel _$MemberModelFromJson(Map<String, dynamic> json) => MemberModel(
      json['Id'] as int,
      json['AdSoyad'] as String,
      json['Eposta'] as String,
    );

Map<String, dynamic> _$MemberModelToJson(MemberModel instance) =>
    <String, dynamic>{
      'Id': instance.Id,
      'AdSoyad': instance.AdSoyad,
      'Eposta': instance.Eposta,
    };
