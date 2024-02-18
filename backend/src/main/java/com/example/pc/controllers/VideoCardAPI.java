package com.example.pc.controllers;

import com.example.pc.Models.VideoCardModel;
import com.example.pc.Services.VideoCardService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api")
public class VideoCardAPI {

    private static final Logger logger = LoggerFactory.getLogger(MotherBoardAPI.class);

    @Autowired
    private VideoCardService videoCardService;

    @PostMapping("/createComponent/VideoCard")
    public ResponseEntity<Map<String, Object>> createVideoCard(@RequestParam("brand") String brand,
                                                               @RequestParam("model") String model,
                                                               @RequestParam("description") String description,
                                                               @RequestParam("price") Integer price,
                                                               @RequestParam("country") String country,
                                                               @RequestParam("memory_db") Integer memory_db,
                                                               @RequestParam("memory_type") String memory_type,
                                                               @RequestParam("image") MultipartFile image){

        try{

            VideoCardModel videoCardModel = new VideoCardModel();
            videoCardModel.setBrand(brand);
            videoCardModel.setModel(model);
            videoCardModel.setDescription(description);
            videoCardModel.setPrice(price);
            videoCardModel.setCountry(country);
            videoCardModel.setMemory_db(memory_db);
            videoCardModel.setMemory_type(memory_type);
            videoCardModel.setImage(image.getBytes());

            Map<String, Object> MotherBoardResponse = new HashMap<>();
            MotherBoardResponse.put("brand", brand);
            MotherBoardResponse.put("model", model);
            MotherBoardResponse.put("description", description);
            MotherBoardResponse.put("price", price);
            MotherBoardResponse.put("country", country);
            MotherBoardResponse.put("memory_db", memory_db);
            MotherBoardResponse.put("momory_type", memory_type);
            MotherBoardResponse.put("image", image.getBytes());

            logger.info("MotherBoard created successfully: {}", MotherBoardResponse);

            return new ResponseEntity<>(MotherBoardResponse, HttpStatus.CREATED);

        }catch(IOException e){
            e.printStackTrace();

            logger.error("Failed to create MotherBoard", e);

            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }
}
