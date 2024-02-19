package com.example.pc.controllers;

import com.example.pc.Models.VideoCardModel;
import com.example.pc.Services.VideoCardService;
import com.example.pc.dto.VideoCardDto;
import lombok.AllArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

@RestController
@AllArgsConstructor
public class VideoCardAPI {

    private static final Logger logger = LoggerFactory.getLogger(MotherBoardAPI.class);

    @Autowired
    private VideoCardService videoCardService;

    @PostMapping("/createComponent/VideoCard")
    public ResponseEntity<VideoCardModel> createVideoCard(@RequestBody VideoCardDto videoCardDto){


        MultipartFile image = videoCardDto.getImage();

        if (image == null || image.isEmpty()) {
            logger.warn("No image provided");
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        logger.info("VideoCard created successfully: {}", videoCardDto);

        return new ResponseEntity<>(videoCardService.createVideoCard(videoCardDto),
                HttpStatus.CREATED);

    }
}
