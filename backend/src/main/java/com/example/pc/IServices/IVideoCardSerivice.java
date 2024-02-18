package com.example.pc.IServices;

import com.example.pc.Models.VideoCardModel;

import java.util.List;

public interface IVideoCardSerivice {

    List<VideoCardModel> getVideoCards();
    VideoCardModel getVideoCardById(Long id);
    VideoCardModel addVideoCard(VideoCardModel videoCardModel);
    void deleteVideoCardById(Long id);
}
