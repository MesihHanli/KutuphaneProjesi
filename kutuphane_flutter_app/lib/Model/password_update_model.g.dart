// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'password_update_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PasswordUpdateModel _$PasswordUpdateModelFromJson(Map<String, dynamic> json) =>
    PasswordUpdateModel(
      json['OldPassword'] as String,
      json['NewPassword'] as String,
    );

Map<String, dynamic> _$PasswordUpdateModelToJson(
        PasswordUpdateModel instance) =>
    <String, dynamic>{
      'OldPassword': instance.OldPassword,
      'NewPassword': instance.NewPassword,
    };
